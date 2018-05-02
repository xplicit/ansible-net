using System;

namespace Ansible
{
    public class Playbook : AnsibleCommand
    {
        public override string CommandName => "ansible-playbook";

        public new Playbook Version() => (Playbook) base.Version();
    }
}
