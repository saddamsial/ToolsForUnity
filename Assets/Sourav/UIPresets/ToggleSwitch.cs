﻿using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Editable.ToggleRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sourav.UIPresets
{
	[RequireComponent(typeof(Button))]
	public class ToggleSwitch : GameElement
	{
		[SerializeField] private ToggleType type;
		[SerializeField] private ToggleHolder[] togglesHolder;
		[SerializeField] private ToggleStatus status;
		
		public void Toggle()
		{
			ChangeToggleStatus();
		}

		public void SetOn()
		{
			status = ToggleStatus.On;
			ToggleHolder toggleHolder = GetCorrectToggle(ToggleStatus.Off);

			for (int i = 0; i < toggleHolder.objects.Length; i++)
			{
				toggleHolder.objects[i].Hide();
			}

			toggleHolder = GetCorrectToggle(ToggleStatus.On);
			
			for (int i = 0; i < toggleHolder.objects.Length; i++)
			{
				toggleHolder.objects[i].Show();
			}
			
			NotificationParam toggle = new NotificationParam(Mode.intData);
			toggle.intData.Add((int)type);
			App.GetNotificationCenter().Notify(Notification.ToggleOn, toggle);
			
		}

		public void SetOff()
		{
			status = ToggleStatus.Off;
			ToggleHolder toggleHolder = GetCorrectToggle(ToggleStatus.Off);

			for (int i = 0; i < toggleHolder.objects.Length; i++)
			{
				toggleHolder.objects[i].Show();
			}

			toggleHolder = GetCorrectToggle(ToggleStatus.On);

			for (int i = 0; i < toggleHolder.objects.Length; i++)
			{
				toggleHolder.objects[i].Hide();
			}
			
			NotificationParam toggle = new NotificationParam(Mode.intData);
			toggle.intData.Add((int)type);
			App.GetNotificationCenter().Notify(Notification.ToggleOff, toggle);
		}

		private ToggleHolder GetCorrectToggle(ToggleStatus type)
		{
			ToggleHolder toggleHolder = null;

			for (int i = 0; i < togglesHolder.Length; i++)
			{
				if (togglesHolder[i].status == type)
				{
					toggleHolder = togglesHolder[i];
					break;
				}
			}
			
			return toggleHolder;
		}

		private void ChangeToggleStatus()
		{
			if (status == ToggleStatus.On)
			{
				SetOff();
			}
			else
			{
				SetOn();
			}
		}
	}

	public enum ToggleStatus
	{
		On,
		Off
	}

	[System.Serializable]
	public class ToggleHolder
	{
		public ToggleStatus status;
		public GameObject[] objects;
	}
}
