using System;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model.Tests
{
    public static class TestSetup
    {
        public static IApplication GetApplication()
        {
            var app = new Application
            {
                Id = Guid.NewGuid(),
                IsFixed = true,
            };

            SetCultures(app);
            SetGlobalFields(app);
            SetEntityTypes(app);

            return app;
        }

        private static void SetCultures(IApplication app)
        {
            app.Cultures.Add(new Culture
            {
                Application = app,
                Code = "fr-FR",
                Id = Guid.NewGuid(),
                IsFixed = false,
            });
            app.Cultures.Add(new Culture
            {
                Application = app,
                Code = "en-US",
                Id = Guid.NewGuid(),
                IsFixed = false,
            });
        }

        private static void SetGlobalFields(IApplication app)
        {
            // TODO
        }

        private static void SetEntityTypes(IApplication app)
        {
            // TODO
        }
    }
}
