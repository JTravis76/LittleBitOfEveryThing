

```cs
[Authorize]
[HttpGet("checksheet/{room}")]
public async Task<IActionResult> CheckSheet(string room)
{
    using (var db = _context)
    {
        Models.CheckSheetReportModel model = new Models.CheckSheetReportModel {
            Room = room,
            Containers = db.Containers.Where(x => x.LOC == room)
        };

        string templateHeader = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Reports\\CheckSheet\\header.cshtml");
        string templateBody = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Reports\\CheckSheet\\body.cshtml");

        string template = await _razorEngine.CompileRenderAsync(templateBody, model);
        string header = await _razorEngine.CompileRenderAsync(templateHeader, model);

        var converter = new PORTS.PdfGenerator.HtmlToPdfConverter()
        {
            Orientation = PORTS.PdfGenerator.PageOrientation.Landscape,
            Margins = new PORTS.PdfGenerator.PageMargins() { Top = 20 },
            PageHeaderHtml = header,
            CustomWkHtmlArgs = string.Format("--footer-right {0} --footer-line --footer-spacing {1}", "\"Page [page] of [topage]\"", 2.2)
        };

        byte[] pdf = converter.GeneratePdf(template);

        return File(pdf, "application/pdf");
    }
}
```

Special note when using RazorLight

```xml
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <MvcRazorCompileOnPublish>false</MvcRazorCompileOnPublish>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="RazorLight" Version="2.0.0-beta1" />
  </ItemGroup>

  <ItemGroup>
    <Content Update="wwwroot\scss\**\*.*" CopyToPublishDirectory="Never" />
    <Content Update="wwwroot\typings\**\*.*" CopyToPublishDirectory="Never" />
    <Content Update="wwwroot\tsconfig.json" CopyToPublishDirectory="Never" />
    <Content Update="Reports\A630\body.cshtml">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="Reports\A630\header.cshtml">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="Reports\CheckSheet\header.cshtml">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="Reports\CheckSheet\body.cshtml">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>    
  </ItemGroup>

  <Target Name="CopyReports" AfterTargets="Publish">
    <Copy SourceFiles="Reports\A630\body.cshtml" DestinationFolder="$(PublishDir)\Reports\A630" />
    <Copy SourceFiles="Reports\A630\header.cshtml" DestinationFolder="$(PublishDir)\Reports\A630" />
    <Copy SourceFiles="Reports\CheckSheet\body.cshtml" DestinationFolder="$(PublishDir)\Reports\CheckSheet" />
    <Copy SourceFiles="Reports\CheckSheet\header.cshtml" DestinationFolder="$(PublishDir)\Reports\CheckSheet" />
  </Target>
```