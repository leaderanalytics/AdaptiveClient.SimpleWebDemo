﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptiveClient.WebDemo
{
    public interface IUsersService
    {
        void SaveUser(User user);
        User GetUserByID(int id);
    }
}
