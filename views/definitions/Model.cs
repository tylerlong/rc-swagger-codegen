public partial class {{ name }}
{
    {% for field in fields %}
    {{ field }}
    {% endfor %}
}