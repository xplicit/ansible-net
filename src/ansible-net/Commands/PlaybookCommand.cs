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

    public AnsiblePlayResult Play()
    {
        var json = base.Execute();

        var result = JsonConvert.DeserializeObject<AnsiblePlayResult>(json, new JsonSerializerSettings{ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }});

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
