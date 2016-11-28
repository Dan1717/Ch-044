﻿using Model.DB;
using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IGoodManager
    {
        void InsertGood(GoodDTO good);
        void InsertGood(Good good);
        List<GoodDTO> GetAll();
        GoodDTO Insert(GoodDTO good);
    }
}