using System;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Tracker;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public class SceneTrackedManager : TrackedManager<IScene> {
		public IServiceProvider ServiceProvider { get; }
		public SceneTrackedManager(IServiceProvider serviceProvider) {
			ServiceProvider = serviceProvider;
		}
		protected override ITracker<IScene> CreateTracked(IScene value) {
			return new SceneTracker(value);
		}
		protected override CreateManagerItem GetCreateManagerItem(ITracker<IScene> tracker) {
			return new CreateManagerItem {
				Id = tracker.Id,
				ManagerName = GetManagerName(),
				Values = new[] {
					new PropertyValue(nameof(IScene.Enabled), tracker.Value.Enabled),
					new PropertyValue(nameof(IScene.Visible), tracker.Value.Visible)
				}
			};
		}
		protected override string GetManagerName() {
			return nameof(SceneTrackedManager);
		}
		protected override IScene CreateItem(CreateManagerItem createManagerItem) {
			return ServiceProvider.GetService<IScene>();
		}
		protected override void OnTrackerItemCreated(CreateManagerItem createManagerItem, ITracker<IScene> tracker) {
			base.OnTrackerItemCreated(createManagerItem, tracker);
			tracker.Value.Initialize();
		}
	}
}
