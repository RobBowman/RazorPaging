using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Utility.Error.Application.Interfaces;
using Utility.Error.Persistence.Implementation;
using Utility.Error.Testing.Unit.Controller;

namespace Utility.Error.Testing.Unit
{
    /// <summary>
    /// When Acquiring Valuation From Repository.
    /// </summary>
    [TestClass]
    public class WhenAcquiringValuationFromRepository
    {
        private static IServiceProvider _serviceProvider;
        private const string Identifier01 = "d11337c1-d222-49b6-aaa0-ff443b6ddd36";
        //private const string Identifier02 = "01d6137f-0904-4138-8ecb-49271021cc8b";
        //private const string Identifier03 = "38fd3dcb-ced0-45c6-b3a7-3c3b561761b1";
        //private const string Identifier04 = "9e31302c-c56b-4b0c-b185-c810f3c0f28e";

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
        public void UN_APP_02_ThenSingleErrorCanBeReadWithIdentifier()
        {
            // Arrange.
            using (var scope = _serviceProvider.CreateScope())
            {
                // Act.
                var uowimplementation = scope.ServiceProvider.GetService<IUnitOfWork>().As<InMemoryUow>();
                var error = uowimplementation.Errors.GetErrorByIdentifier(Identifier01);

                // Assert.
                error.Should().NotBeNull();
                error.Id.Should().Be(1);
            }
        }

        #endregion
    }
}
