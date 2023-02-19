internal class Program
{
    private static void Main(string[] args)
    {
        var transmissorBluetooth = TransmissorFactory.Transmissor(TipoTransmissao.Bluetooth)
                                        .CriarTransmissorComando("endereco:porta");

        transmissorBluetooth.AbrirTransmissao();

        transmissorBluetooth.EnviarComando("Comando BL1");
        transmissorBluetooth.EnviarComando("Comando BL2");

        Console.WriteLine($"Comandos enviados por BLUETOOTH:\n{transmissorBluetooth.RetornarComandosExecutados()}");
        transmissorBluetooth.FecharTransmissao();

        Console.WriteLine("\n\n=================================\n\n");

        var transmissorUSB = TransmissorFactory.Transmissor(TipoTransmissao.USB)
                                .CriarTransmissorComando("endereco:porta");

        transmissorUSB.AbrirTransmissao();

        transmissorUSB.EnviarComando("Comando US1");
        transmissorUSB.EnviarComando("Comando US2");
        transmissorUSB.EnviarComando("Comando US3");

        Console.WriteLine($"Comandos enviados por USB:\n{transmissorUSB.RetornarComandosExecutados()}");
        transmissorUSB.FecharTransmissao();

        Console.ReadKey();
    }
}


public enum TipoTransmissao
{
    Bluetooth,
    USB
}

public abstract class Transmissor
{
    protected readonly List<string> ComandosExecutados;
    protected Transmissor(string endereco)
    {
        Endereco = endereco;
        ComandosExecutados = new List<string>();
    }

    protected string Endereco { get; set; }

    public abstract void AbrirTransmissao();
    public abstract void FecharTransmissao();
    public abstract void EnviarComando(string comando);
    public string RetornarComandosExecutados()
    {
        return string.Join("\n", ComandosExecutados);
    }
}

public class TransmissorBluetoothFactory : TransmissorFactory
{
    public override Transmissor CriarTransmissorComando(string endereco)
    {
        return new TransmissorComandoBluetooth(endereco);
    }
}

public class TransmissorComandoBluetooth : Transmissor
{
    public TransmissorComandoBluetooth(string endereco) : base(endereco)
    {
        Endereco = endereco;
    }

    public override void AbrirTransmissao()
    {
        Console.WriteLine("Transmissão BLUETOOTH aberta.");
    }

    public override void FecharTransmissao()
    {
        Console.WriteLine("Transmissão BLUETOOTH fechada.");
    }

    public override void EnviarComando(string comando)
    {
        ComandosExecutados.Add(comando);
    }
}


public class TransmissorComandoUSB : Transmissor
{
    public TransmissorComandoUSB(string endereco) : base(endereco)
    {
        Endereco = endereco;
    }

    public override void AbrirTransmissao()
    {
        Console.WriteLine("Transmissão USB aberta.");
    }

    public override void FecharTransmissao()
    {
        Console.WriteLine("Transmissão USB fechada.");
    }
    public override void EnviarComando(string comando)
    {
        ComandosExecutados.Add(comando);
    }
}

public abstract class TransmissorFactory
{
    public abstract Transmissor CriarTransmissorComando(string endereco);

    public static TransmissorFactory Transmissor(TipoTransmissao tipoTransmissao)
    {
        if (tipoTransmissao == TipoTransmissao.Bluetooth)
            return new TransmissorBluetoothFactory();
        if (tipoTransmissao == TipoTransmissao.USB)
            return new TransmissorUSBFactory();

        throw new ApplicationException("Tipo de comunicação inválido.");
    }
}

public class TransmissorUSBFactory : TransmissorFactory
{
    public override Transmissor CriarTransmissorComando(string endereco)
    {
        return new TransmissorComandoUSB(endereco);
    }
}