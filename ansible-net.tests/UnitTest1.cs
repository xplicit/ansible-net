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

            var playbook = new Playbook().Version();
            
            playbook.Execute(x => sb.AppendLine(x));

            Assert.That(sb.ToString(), Does.Contain("ansible-playbook"));
        }
    }
}