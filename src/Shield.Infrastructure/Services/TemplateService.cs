using RazorLight;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Provides services for rendering templates using RazorLight.
/// </summary>
public class TemplateService
{
    private readonly RazorLightEngine _engine;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateService"/> class.
    /// </summary>
    public TemplateService()
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
        _engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(templatePath)
            .UseMemoryCachingProvider()
            .Build();
    }

    /// <summary>
    /// Renders a template with the specified model asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <param name="templateName">The name of the template.</param>
    /// <param name="model">The model to render the template with.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the rendered template as a string.</returns>
    public async Task<string> RenderTemplateAsync<T>(string templateName, T model)
    {
        string templatePath = $"{templateName}.cshtml";
        string emailBody = await _engine.CompileRenderAsync(templatePath, model);

        return emailBody;
    }
}
