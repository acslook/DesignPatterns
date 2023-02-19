// Criando subject (Sensor) e inserindo os Observers (Cliente)
var cliente1 = new Cliente("Cliente1");
var cliente2 = new Cliente("Cliente2");

Temperatura temperatura = new Temperatura(120.00, "C");
temperatura.Attach(cliente1);
temperatura.Attach(cliente2);
// Simulando mudança de temperatura e notificando os clientes
temperatura.Valor = 120.10;
temperatura.Valor = 121.00;
temperatura.Valor = 120.50;
temperatura.Valor = 120.75;

// Criando subject (Sensor) e inserindo os Observers (Cliente)
Humidade humidade = new Humidade(120.00, "%");
humidade.Attach(cliente1);
humidade.Attach(cliente2);
// Simulando mudança de humidade e notificando os clientes
humidade.Valor = 97;
humidade.Valor = 80;

// Removendo um observer
humidade.Detach(cliente2);
humidade.Valor = 50;
humidade.Valor = 35;

Console.ReadKey();


/// <summary>
/// 'Subject' abstract class
/// </summary>
public abstract class Sensor
{
    private SensorTipo tipo;
    private string medida;
    private double valor;
    private List<ICliente> clientes = new List<ICliente>();

    public Sensor(SensorTipo tipo, double valor, string medida)
    {
        this.tipo = tipo;
        this.medida = medida;
        this.valor = valor;
    }

    public void Attach(ICliente cliente)
    {
        clientes.Add(cliente);
    }

    public void Detach(ICliente cliente)
    {
        clientes.Remove(cliente);
    }

    public void Notify()
    {
        foreach (ICliente clientes in clientes)
        {
            clientes.Atualizar(this);
        }
        Console.WriteLine("");
    }

    public double Valor
    {
        get { return valor; }
        set
        {
            if (valor != value)
            {
                valor = value;
                Notify();
            }
        }
    }

    public string Medida
    {
        get { return medida; }
    }

    public SensorTipo Tipo
    {
        get { return tipo; }
    }
}
/// <summary>
/// The 'ConcreteSubject' class
/// </summary>
public class Temperatura : Sensor
{
    // Constructor
    public Temperatura(double valor, string medida)
        : base(SensorTipo.Temperatura, valor, medida)
    {
    }
}

/// <summary>
/// The 'ConcreteSubject' class
/// </summary>
public class Humidade : Sensor
{
    // Constructor
    public Humidade(double valor, string medida)
        : base(SensorTipo.Humidade, valor, medida)
    {
    }
}
/// <summary>
/// The 'Observer' interface
/// </summary>
public interface ICliente
{
    void Atualizar(Sensor sensor);
}

public enum SensorTipo
{
    Temperatura,
    Humidade,
    Pressao
}

/// <summary>
/// The 'ConcreteObserver' class
/// </summary>
public class Cliente : ICliente
{
    private string nome;
    private Sensor sensor;
    // Constructor
    public Cliente(string nome)
    {
        this.nome = nome;
    }
    public void Atualizar(Sensor sensor)
    {
        Console.WriteLine($"Sensor {sensor.Tipo} notificando novo valor para {nome}: {sensor.Valor} {sensor.Medida}");
    }
    // Gets or sets the stock
    public Sensor Sensor
    {
        get { return sensor; }
        set { sensor = value; }
    }
}