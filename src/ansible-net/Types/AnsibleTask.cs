using System.Collections.Generic;

namespace Ansible.Types;

public class AnsibleTask
{
    public AnsibleTaskDefinition Task { get; set; }
    public Dictionary<string, Dictionary<string, object>> Hosts { get; set; }
}
