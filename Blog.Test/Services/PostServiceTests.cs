using Blog.Application.Hubs;
using Blog.Application.Services;
using Blog.Domain.DTOs.Post;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Model;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace Blog.Test.Services;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IHubContext<PostNotificationHub>> _hubContextMock;
    private readonly Mock<IClientProxy> _clientProxyMock;
    private readonly PostService _service;

    public PostServiceTests()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _hubContextMock = new Mock<IHubContext<PostNotificationHub>>();
        _clientProxyMock = new Mock<IClientProxy>();

        var clientsMock = new Mock<IHubClients>();
        clientsMock.Setup(c => c.All).Returns(_clientProxyMock.Object);
        _hubContextMock.Setup(h => h.Clients).Returns(clientsMock.Object);

        _service = new PostService(_postRepositoryMock.Object, _hubContextMock.Object);
    }

    [Fact]
    public async Task CreatePostAsync_ShouldInsertAndNotify()
    {
        // Arrange
        var postDto = new PostCreateDto { Title = "Test Title" };
        var postEntity = new Post { Id = 1, Title = "Test Title" };
        _postRepositoryMock.Setup(r => r.InsertAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Callback<Post, CancellationToken>((p, _) => p.Id = 1);

        _clientProxyMock.Setup(c => c.SendCoreAsync(
            "NewPost",
            It.Is<object[]>(o => (int)o[0] == 1 && (string)o[1] == $"Novo post criado: {postEntity.Title}"),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.CreatePostAsync(postDto, It.IsAny<int>(), CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
        _clientProxyMock.Verify(c => c.SendCoreAsync(
            "NewPost",
            It.IsAny<object[]>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeletePostAsync_ShouldCallRepository()
    {
        // Arrange
        int postId = 1;
        _postRepositoryMock.Setup(r => r.DeleteAsync(postId, It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _service.DeletePostAsync(postId, It.IsAny<int>(), CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(r => r.DeleteAsync(postId, It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllPostsAsync_ShouldReturnDtosAndCount()
    {
        // Arrange
        var filter = new PostFilterDto { UserId = 1, Page = 1, PageSize = 2 };
        var posts = new List<Post> {
            new Post { Id = 1, Title = "A" },
            new Post { Id = 2, Title = "B" }
        };
        int count = posts.Count;
        _postRepositoryMock.Setup(r => r.GetAllAsync(filter.UserId, filter.Page, filter.PageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync((count, posts));

        // Act
        var (resultDtos, resultCount) = await _service.GetAllPostsAsync(filter, CancellationToken.None);

        // Assert
        Assert.Equal(count, resultCount);
        Assert.Equal(posts.Select(p => p.Title), resultDtos.Select(d => d.Title));
        _postRepositoryMock.Verify(r => r.GetAllAsync(filter.UserId, filter.Page, filter.PageSize, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdatePostAsync_ShouldCallRepository()
    {
        // Arrange
        var postDto = new PostUpdateDto { Id = 1, Title = "Updated" };
        _postRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _service.UpdatePostAsync(postDto, It.IsAny<int>(), CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}