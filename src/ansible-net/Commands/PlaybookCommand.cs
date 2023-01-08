using System.IO;
using Ansible.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ansible.Commands;

public class PlaybookCommand : AnsibleCommand
{
    public override string CommandName => "ansible-playbook";

    public string Name { get; }

    public PlaybookCommand(string name)
    {
        Name = name;
    }

    public AnsiblePlayResult Play(Stream errors = null)
    {
        using var outputStream = new MemoryStream();
        
        base.Execute(outputStream, errors, format: OutputFormat.Json);

        var serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });

        outputStream.Position = 0;
        using var streamReader = new StreamReader(outputStream);
        using var jsonTextReader = new JsonTextReader(streamReader);

        var result = serializer.Deserialize<AnsiblePlayResult>(jsonTextReader);

        return result;
    }

    protected override string CreateCommandLine()
    {
        var args = base.CreateCommandLine();

        if (args.Length > 0)
            args = args + " " + Name;
        else
            args = Name;

        return args;
    }
}
