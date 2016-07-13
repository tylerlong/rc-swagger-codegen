public partial class {{ segment | pascalCase }}
{
    {% for method in methods %}
    public void {{ method }}()
    {

    }
    {% endfor %}

    {% for model in models %}
    public {{ model | pascalCase }} {{ model | pascalCase }}()
    {
        return new {{ model | pascalCase }}();
    }
    {% endfor %}
}