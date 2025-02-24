﻿using Domain.Models;

namespace Application.Response
{
    public class TransferResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransferStatusResponse Status { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public Guid SrcAccountId { get; set; }
        //public Guid DestAccountId { get; set; }
        public string DestAccountAliasOrCBU { get; set; }
    }
}
