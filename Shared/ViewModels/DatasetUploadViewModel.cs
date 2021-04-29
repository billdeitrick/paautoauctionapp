using System.Collections.Generic;
using System;

namespace paautoauctionapp.Shared.ViewModels
{

    public class DatasetUploadViewModel
    {

        public DateTime SaleDate { get; set; }
        public List<SaleEntryViewModel> SaleEntries { get; set; }

    }

}