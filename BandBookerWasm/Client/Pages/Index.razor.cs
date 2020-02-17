using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using BandBookerWasm.Shared.Models;
using Microsoft.AspNetCore.Components;
using BandBookerWasm.Shared;
using DevExpress.Blazor;

namespace BandBookerWasm.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public BandBookerDataManager DataMan { get; set; }

        List<Instrument> DummySelection { get; set; } = new List<Instrument>(); //<-- for testing the object picker

        /* Instruments code */

        public List<Instrument> Instruments { get => DataMan.Instruments; }

        protected void InstrumentInsertAction(IDictionary<string, object> values)
        {            
            Instruments.Add(values.AssignToObject(new Instrument() { Id = Instruments.Count() + 1 }));
        }
		protected void InstrumentUpdateAction(Instrument instrument, IDictionary<string, object> newValues)
        {
            newValues.AssignToObject(instrument);
        }

        protected void InstrumentRemoveAction(Instrument instrument)
        {
            Instruments.Remove(instrument);
        }

        /* Musicians code */
        DxDataGrid<Musician> gridMusicians;
        public List<Musician> Musicians { get => DataMan.Musicians; }

		#region Edit model to be used in the editor of the grid 
		class MusicianEditModel
        {
            public MusicianEditModel(Musician musician)
            {
                Musician = musician;
                if (musician == null)
                {
                    Musician = new Musician();
                    IsNewRow = true;
                }
                
                Name = Musician.Name;
                Instruments = new List<Instrument>(Musician.Instruments);                
            }

            public Action StateHasChanged { get; set; }
            public Musician Musician { get; set; }
            public bool IsNewRow { get; set; }
            
            public List<Instrument> AllInstruments { get; set; }

            // model properties
            [Required]
            public string Name { get; set; }
            public IEnumerable<Instrument> Instruments { get; set; }            
            
        }
		#endregion
		
        MusicianEditModel musicianModel = null;

        protected void OnMusicianEditing(Musician musician)
        {
            musicianModel = new MusicianEditModel(musician);
            musicianModel.StateHasChanged += () => { InvokeAsync(StateHasChanged); };
        }
        protected void OnMusicianRemoving(Musician musician)
        {
            Musicians.Remove(musician);
        }

        Guid keyMusicians = Guid.NewGuid();
        protected void MusicianSubmitAction()
        {
            musicianModel.Musician.Name = musicianModel.Name;
            musicianModel.Musician.Instruments = musicianModel.Instruments.ToList();
            if (musicianModel.IsNewRow)
            {
                DataMan.Musicians.Add(musicianModel.Musician);
                keyMusicians = Guid.NewGuid();

            }
            musicianModel = null;
            gridMusicians.CancelRowEdit();
        }

        void OnMusicianCancelButtonClick()
        {
            musicianModel = null;
            gridMusicians.CancelRowEdit();
        }        


        
    }
}