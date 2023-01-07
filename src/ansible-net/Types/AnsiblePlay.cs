using System.Collections.Generic;

namespace Ansible.Types;

public class AnsiblePlay
{
    public AnsibleTaskDefinition Play { get; set; }
    public List<AnsibleTask> Tasks { get; set; }
}
