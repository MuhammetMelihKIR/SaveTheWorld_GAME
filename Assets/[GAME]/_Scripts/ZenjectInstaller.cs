using Managers;
using Zenject;
public class ZenjectInstaller : MonoInstaller
{
    public GameManager gameManager;
    public DeckManager deckManager;
    public WorldHealth worldHealth;
    public DiceManager diceManager;
    public TweenManager tweenManager;
    public AudioManager audioManager; 
    
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<DeckManager>().FromInstance(deckManager).AsSingle();
        Container.Bind<WorldHealth>().FromInstance(worldHealth).AsSingle();
        Container.Bind<DiceManager>().FromInstance(diceManager).AsSingle();
        Container.Bind<TweenManager>().FromInstance(tweenManager).AsSingle();
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
    }
}
