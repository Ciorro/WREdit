using WREdit.Base.Actions;
using WREdit.Base.Attributes;
using WREdit.Base.Models;

namespace WREditActions
{
    enum GroundType
    {
        Runway, Asphalt, Gravel, Mud, None
    }

    [GameObjectAction(DisplayName = "Test action")]
    internal class TestAction : IGameObjectAction
    {
        [GameObjectActionProperty(typeof(string))]
        public string? TestString { get; set; }

        [GameObjectActionProperty(typeof(int), "Test Int")]
        public int? TestInt { get; set; }

        [GameObjectActionProperty(typeof(GroundType), "Test enum (ground)")]
        public GroundType? Ground { get; set; }

        [GameObjectActionProperty(typeof(bool), "Bul bul")]
        public bool? TestBoolean { get; set; }

        public void Execute(GameObject gameObject)
        {
            Console.WriteLine("Executed");
        }
    }
}
