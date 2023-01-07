using System.Collections.Generic;

namespace Ansible.Types;

public class AnsiblePlayResult
{
    public object CustomStats { get; set; }
    public object GlobalCustomStats { get; set; }
    public List<AnsiblePlay> Plays { get; set; }
}
