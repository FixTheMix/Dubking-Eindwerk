using GalaSoft.MvvmLight.Ioc;


namespace DialogueService
{
    public class DialogueModule
    {
        public static void RegisterTypes()
        {
            SimpleIoc.Default.Register<IShowDialogue, DialogueBoxFactory>();
        }
    }
}
