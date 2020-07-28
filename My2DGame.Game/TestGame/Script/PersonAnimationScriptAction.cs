using My2DGame.Component.Animation;
using My2DGame.Component.Script;
using My2DGame.Core.GameObject;

namespace My2DGame.Game.TestGame.Script {
	public class PersonAnimationScriptAction : BaseScriptAction {
		private AnimationComponent AnimationComponent { get; set; }
		private PersonScriptAction PersonScriptAction { get; set; }
		public override void Initialize(IGameObject gameObject) {
			base.Initialize(gameObject);
			AnimationComponent = gameObject.Components.Get<AnimationComponent>();
			var scriptComponent = gameObject.Components.Get<ScriptComponent>();
			PersonScriptAction = scriptComponent.GetScriptAction<PersonScriptAction>();
		}

	}
}
