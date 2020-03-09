using NUnit.Framework;
using System.Text;

namespace Ansible.Tests
{
    public class AnsibleCommandTests
    {
        [Test]
        public void OutputCommand()
        {
            var sut = new AdHoc();

            string cmd = sut.ToString();

            Assert.AreEqual(sut.CommandName, cmd);
        }

        [Test]
        public void OutputCommandWithArgs()
        {
            string arg0 = "all";
            string arg1 = "--module-name";
            string arg2 = "setup";
            var sut = new AdHoc();
            sut.AddParameter(arg0);
            sut.AddParameter(arg1, arg2);
            string expectedCmd = $"{sut.CommandName} {arg0} {arg1} {arg2}";

            string cmd = sut.ToString();

            Assert.AreEqual(expectedCmd, cmd);
        }

    }
}