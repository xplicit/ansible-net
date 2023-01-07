namespace Ansible.Types;

public class AnsibleTaskDefinition
{
    public string Name { get; set; }
    public string Id { get; set; }
    public Duration Duration { get; set; }
}
