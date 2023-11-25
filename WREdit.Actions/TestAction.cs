using WREdit.Base.Actions;
using WREdit.Base.Attributes;
using WREdit.Base.Models;

namespace WREditActions
{
    public enum GroundType
    {
        Runway, Asphalt, Gravel, Mud, None
    }

    [GameObjectAction(DisplayName = "Test action 1")]
    internal class TestAction : IGameObjectAction
    {
        public string? TestString { get; set; }

        [GameObjectActionProperty]
        public float? TestSingle { get; set; }
        public double TestDouble { get; set; }

        [GameObjectActionProperty("Liczba całkowita")]
        public int? TestInt { get; set; }

        [GameObjectActionProperty("Typ wyliczeniowy")]
        public GroundType? Ground { get; set; }

        [GameObjectActionProperty("Wartość logiczna")]
        public bool? TestBoolean { get; set; }

        public void Execute(GameObject gameObject)
        {
            Console.WriteLine("Executed");
        }
    }
}
