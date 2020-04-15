using BandBookerWasm.Shared;
using BandBookerWasm.Shared.Models;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandBookerWasm.Client.Shared
{
	public partial class DXEventEditor
	{
		public DXEventEditor()
		{
			CalendarStorage = new DxSchedulerDataStorage()
			{
				AppointmentsSource = new List<Event>(),
				AppointmentMappings = new DxSchedulerAppointmentMappings()
				{
					//Type = "AppointmentType",
					Start = nameof(Event.StartDate),
					End = nameof(Event.EndDate),
					Subject = nameof(Event.Name),
					Description = nameof(Event.Description),
					AllDay = nameof(Event.AllDay),
					StatusId = nameof(Event.Status)
				}
			};
		}

		[Parameter]
		public List<Event> Events { get; set; }
		[Parameter]
		public List<Session> Sessions { get; set; }

		public bool ShowCalendar { get; set; };
		public Action<bool> Update(Action<bool> set)
		{
			return (v) => { set(v); InvokeAsync(StateHasChanged); };
		}
		protected override void OnParametersSet()
		{
			base.OnParametersSet();
			CalendarStorage.AppointmentsSource = Events;
			CalendarStorage.RefreshData();
		}

		/* Grid methods */

		protected void InsertAction(IDictionary<string, object> values)
		{
			Events.Add(values.AssignToObject(new Event() { Id = Events.Count <= 0 ? 1 : Events.Max(x => x.Id) + 1 }));
		}
		protected void UpdateAction(Event e, IDictionary<string, object> newValues)
		{
			newValues.AssignToObject(e);
		}

		protected void RemoveAction(Event e)
		{
			Events.Remove(e);
		}

		/* Calendar Methods */
		
		public DxSchedulerDataStorage CalendarStorage { get; set; }
	}
}
