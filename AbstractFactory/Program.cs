var componentesAndroid = ComponentesAbstractFactory.ObterInstanciaComponentes(TipoSistema.Android);
BotaoAbstract botaoA = componentesAndroid.CriarBotao("BotaoA");
BotaoAbstract botaoB = componentesAndroid.CriarBotao("BotaoB");


var componentesIOS = ComponentesAbstractFactory.ObterInstanciaComponentes(TipoSistema.IOS);
BotaoAbstract botao1 = componentesIOS.CriarBotao("Botao1");
BotaoAbstract botao2 = componentesIOS.CriarBotao("Botao2");

Console.ReadKey();


public abstract class BotaoAbstract
{
    public string Nome { get; set; }
    public string Texto { get; set; }
    public double Altura { get; set; }
    public double Largura { get; set; }
    public BotaoAbstract(string nome)
    {
        Nome = nome;
    }
}

public class BotaoAndroid : BotaoAbstract
{
    public BotaoAndroid(string nome)
        : base(nome)
    {
        Console.WriteLine($"Criado botão Android => {nome}");
    }
}

public class BotaoIOS : BotaoAbstract
{
    public BotaoIOS(string nome)
        : base(nome)
    {
        Console.WriteLine($"Criado botão IOS => {nome}");
    }
}

public class ComponentesAndroid : ComponentesAbstractFactory
{
    public override BotaoAbstract CriarBotao(string nome)
    {
        return new BotaoAndroid(nome);
    }
}

public class ComponentesIOS : ComponentesAbstractFactory
{
    public override BotaoAbstract CriarBotao(string nome)
    {
        return new BotaoIOS(nome);
    }
}

public abstract class ComponentesAbstractFactory
{
    public abstract BotaoAbstract CriarBotao(string nome);

    public static ComponentesAbstractFactory ObterInstanciaComponentes(TipoSistema tipoSistema)
    {
        switch (tipoSistema)
        {
            case TipoSistema.Android:
                return new ComponentesAndroid();
            case TipoSistema.IOS:
                return new ComponentesIOS();
            default:
                throw new ApplicationException("Tipo de sistema inválido.");
        }
    }
}

public enum TipoSistema
{
    Android,
    IOS
}