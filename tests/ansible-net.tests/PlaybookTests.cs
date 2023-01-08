using System.IO;
using NUnit.Framework;
using System.Text;
using Ansible.Commands;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Ansible.Tests;

public class PlaybookTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CheckVersion()
    {
        var sb = new StringBuilder();

        var playbook = new PlaybookCommand("test").Version();
            
        playbook.Execute(x => sb.AppendLine(x));
        var actual = sb.ToString();

        actual.Should().Contain("ansible-playbook");
    }

    [Test]
    public void CheckOutput()
    {
        var sb = new StringBuilder();
        var playbook = new PlaybookCommand("../../../../playbooks/environment.yml");
            
        playbook.Execute(x => sb.AppendLine(x));
        var actual=sb.ToString();

        actual.Should().NotBeEmpty();
    }
        
    [Test]
    public void SetOutputAsJson_ReturnsValidJson()
    {
        var playbook = new PlaybookCommand("../../../../playbooks/hello-world.yml");

        using var outputStream = new MemoryStream();
        
        playbook.Execute(outputStream, format: OutputFormat.Json);
        var json = Encoding.UTF8.GetString(outputStream.ToArray());
            
        var schema = JSchema.Parse("{}");
        var obj = JObject.Parse(json);
        var actual = obj.IsValid(schema);

        actual.Should().BeTrue();
    }
    
    [Test]
    public void Play_ReturnsValidPlayResult()
    {
        var playbook = new PlaybookCommand("../../../../playbooks/hello-world.yml");

        var actual = playbook.Play();

        actual?.Plays.Should().NotBeNull();
        actual.Plays.Should().HaveCount(1);
        actual.Plays[0].Tasks.Should().HaveCount(3);
        actual.Plays[0].Tasks[2].Hosts["localhost"]["echo_output.stdout"].Should().Be("Hello, World!");
    }
    
    [Test]
    public void SyntaxErrorPlaybook_ReturnsErrorMessage()
    {
        var playbook = new PlaybookCommand("../../../../playbooks/syntax-error.yml");

        using var errorStream = new MemoryStream();
        var actual = playbook.Play(errorStream);
        var actualError = Encoding.UTF8.GetString(errorStream.ToArray());

        actualError.Should().Contain("ERROR");
    }
}
