using UnityEngine;

namespace VavilichevGD.Meta.FortuneWheel {
	public class FortuneWheel : MonoBehaviour {

		#region CONSTANTS

		private float CIRCLE_ANGLE = 360f;

		#endregion

		#region DELEGATES

		public delegate void FortuneWheelEventHandler(FortuneWheel fortuneWheel);
		public event FortuneWheelEventHandler OnRotateStartEvent;
		public event FortuneWheelEventHandler OnRotateOverEvent;

		public delegate void FortuneWheelRewardEventHandler(FortuneWheel fortuneWheel, string rewardInfoId);
		public event FortuneWheelRewardEventHandler OnRewardReceivedEvent;

		#endregion

		[SerializeField] private FortuneWheelConfig m_config;

		[SerializeField] private int fullCircles = 5;
		[SerializeField] private float maxLerpRotationTime = 4f;
		[SerializeField] private Transform transformCircle;
		[SerializeField] private AnimationCurve curveRotating;

		private float[] sectorAngles;
		private float currentLerpRotationTime;
		private float finalAngle;
		private float startAngle;

		public bool isRotating { get; protected set; }
		public FortuneWheelConfig config => this.m_config;


		private void Start() {
			this.InitAngles();
		}

		private void InitAngles() {
			this.sectorAngles = new float[this.m_config.sectorsCount];
			for (int i = 0; i < this.m_config.sectorsCount; i++)
				this.sectorAngles[i] = this.m_config.GetSectorData(i).angle;
		}

		public void Rotate() {
			if (!this.isRotating) {
				currentLerpRotationTime = 0f;

				float randomFinalAngle = this.m_config.GetRandomAngle();

				this.finalAngle = -(fullCircles * CIRCLE_ANGLE + randomFinalAngle);
				this.isRotating = true;
				this.OnRotateStartEvent?.Invoke(this);
			}
		}

		private void Update() {
			if (!isRotating)
				return;

			this.currentLerpRotationTime += Time.deltaTime;
			if (this.currentLerpRotationTime >= this.maxLerpRotationTime)
				this.FinishRotating();

			float time = Mathf.Min(currentLerpRotationTime / maxLerpRotationTime, 1f);
			float value = curveRotating.Evaluate(time);
			float angle = Mathf.Lerp(startAngle, finalAngle, value);
			
			transformCircle.eulerAngles = new Vector3(0, 0, angle);
		}

		private void FinishRotating() {
			this.currentLerpRotationTime = this.maxLerpRotationTime;
			this.isRotating = false;
			this.startAngle = this.finalAngle % CIRCLE_ANGLE;
			this.OnRotateOverEvent?.Invoke(this);

			this.ApplyRewardByAngle(this.startAngle);
		}
		
		private void ApplyRewardByAngle(float angle) {
			var clampedAngle = Mathf.Abs(angle);
			var sectorData = this.m_config.GetSectorDataByAngle(clampedAngle);
			this.OnRewardReceivedEvent?.Invoke(this, sectorData.rewardInfo.id);
		}
	}
}