using BandBookerWasm.Shared.Models;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandBookerWasm.Client.Shared
{
	public partial class DXMusicianEditor
	{
        DxDataGrid<Musician> gridMusicians;
        [Parameter]
        public List<Musician> Musicians { get; set; } // => DataMan.Musicians; }
        [Parameter]
        public List<Instrument> Instruments { get; set; }

        public bool UseTagBox { get; set; }
        public Action<bool> Update(Action<bool> set)
        {
            return (v) => { set(v); InvokeAsync(StateHasChanged); };
        }

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
            public IEnumerable<Instrument> Instruments
            {
                // Need to get an IEnumerable for the DXTagBox
                get => InstrumentsList;
                set => InstrumentsList = new List<Instrument>(value);
            }
            public List<Instrument> InstrumentsList { get; set; }
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
                musicianModel.Musician.Id = Musicians.Count <= 0 ? 1 : Musicians.Max(x => x.Id) + 1;
                Musicians.Add(musicianModel.Musician);
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
