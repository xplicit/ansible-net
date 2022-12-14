using Ansible.Commands;
using NUnit.Framework;
using FluentAssertions;

namespace Ansible.Tests;

public class AnsibleCommandTests
{
    [Test]
    public void OutputCommand()
    {
        var sut = new AdHocCommand();

        var actualCmd = sut.ToString();

        actualCmd.Should().Be(sut.CommandName);
    }

    [Test]
    public void OutputCommandWithArgs()
    {
        var arg0 = "all";
        var arg1 = "--module-name";
        var arg2 = "setup";
        var sut = new AdHocCommand();
        sut.AddParameter(arg0);
        sut.AddParameter(arg1, arg2);
        var expectedCmd = $"{sut.CommandName} {arg0} {arg1} {arg2}";

        var actualCmd = sut.ToString();

        actualCmd.Should().Be(expectedCmd);
    }
}
