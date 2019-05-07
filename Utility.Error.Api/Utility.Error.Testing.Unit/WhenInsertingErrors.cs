using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using Utility.Error.Application.Error.Models;
using Utility.Error.Testing.Unit.Controller;


namespace Utility.Error.Testing.Unit
{
    /// <summary>
    /// When Inserting Errors.
    /// </summary>
    [TestClass]
    public class WhenInsertingErrors
    {
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// Initialise.
        /// </summary>
        [TestInitialize]
        public void Initialise()
        {
            // Register Service Types.
            _serviceProvider = Startup.Initialise();
        }

        // Test Methods.
        #region TestMethods

        /// <summary>
        /// Then Support Services Can Be Initialised.
        /// </summary>
        [TestMethod]
        [TestCategory("UNIT")]
        public void UN_APP_01_ThenSupportingServicesCanBeInitialised()
        {
            // Arrange.
            using (var scope = _serviceProvider.CreateScope())
            {
                // Act.
                var controller = scope.ServiceProvider.GetService<BaseController>().As<ErrorController>();

                // Assert.
                controller.Should().NotBeNull();
            }
        }

        /// <summary>
        /// Then Single Error Can Be Read With Identifier.
        /// </summary>
        [TestMethod]
        [TestCategory("UNIT")]
        public void UN_APP_02_ThenSingleErrorCanBeInserted()
        {
            // Arrange.
            using (var scope = _serviceProvider.CreateScope())
            {
                var identifier = "4ba9d58d-7d5c-4629-a45d-5365145098d6";

                // Initialise Single Error.
                var errorIn = new ErrorDetailModel()
                {
                    Identifier = identifier,
                    Category = "Application",
                    DateTimeCreated = DateTime.Now,
                    Severity = "Critical",
                    Details = "Somethings gone wrong!",
                    ErrorCode = "1000",
                    Source = "Atomic.Service.Fca",
                    StackTrace = string.Empty,
                };

                // Act.
                var controller = scope.ServiceProvider.GetService<BaseController>().As<ErrorController>();
                controller.PostError(errorIn);
                var errorOut = controller.GetErrorByIdentifier(identifier);

                // Assert.
                errorOut.Should().NotBeNull();
                errorOut.Identifier.Should().Be(identifier);
            }
        }

        #endregion
    }
}
