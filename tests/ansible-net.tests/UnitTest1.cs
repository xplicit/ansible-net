using System;
using NUnit.Framework;
using Ansible;
using System.Text;

namespace Ansible.Tests
{
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

            var playbook = new Playbook("test").Version();
            
            playbook.Execute(x => sb.AppendLine(x));

            Assert.That(sb.ToString(), Does.Contain("ansible-playbook"));
        }

        [Test]
        public void CheckOutput()
        {
            var sb = new StringBuilder();

            var playbook = new Playbook("../../../../playbooks/environment.yml");
            
            playbook.Execute(x => sb.AppendLine(x));

            var t=sb.ToString();

            Assert.Pass();
        }
    }
}