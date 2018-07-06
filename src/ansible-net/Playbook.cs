using System;

namespace Ansible
{
    public class Playbook : AnsibleCommand
    {
        public override string CommandName => "ansible-playbook";

        public string Name { get; }

        public Playbook(string name)
        {
            Name = name;
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
}
