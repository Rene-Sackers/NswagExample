using System.CommandLine;
using NSwag;
using NSwag.CodeGeneration.CSharp;

var rootCommand = new RootCommand();

var swaggerFile = new Option<FileInfo>("--swaggerFile", "The path to the swagger JSON definition file");
rootCommand.AddOption(swaggerFile);

var output = new Option<FileInfo>("--output", "The path to output the generated file to");
rootCommand.AddOption(output);

rootCommand.SetHandler(
	async (swaggerFileInfo, outputInfo) => await RunAsync(swaggerFileInfo, outputInfo),
	swaggerFile,
	output);

await rootCommand.InvokeAsync(args);

static async Task RunAsync(FileInfo swaggerFile, FileInfo output)
{
	if (Directory.Exists(output.DirectoryName))
	{
		Directory.Delete(output.DirectoryName, true);
	}

	Console.WriteLine("Ensure output directory: " + output.DirectoryName);
	Directory.CreateDirectory(output.DirectoryName);
	
	var swaggerJson = File.ReadAllText(swaggerFile.FullName);
	var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

	var settings = new CSharpClientGeneratorSettings
	{
		ClassName = "ApiClient", 
		CSharpGeneratorSettings = 
		{
			Namespace = "NswagTest.Client"
		}
	};

	var generator = new CSharpClientGenerator(document, settings);	
	var code = generator.GenerateFile();
	File.WriteAllText(output.FullName, code);
}