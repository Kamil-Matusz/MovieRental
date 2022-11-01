using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRental.MVC.Controllers;
using MovieRental.MVC.Services;
using Xunit;

namespace MovieRental.Tests
{
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _mockRepo;
        private readonly MovieController _movieController;

        public MovieControllerTests()
        {
            _mockRepo = new Mock<IMovieService>();
            _movieController = new MovieController(_mockRepo.Object);
        }

        /*
        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _movieController.Index();
            Assert.IsType<ViewResult>(result);
        } */

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _movieController.Create();

            Assert.IsType<ViewResult>(result);
        }

    }
}
