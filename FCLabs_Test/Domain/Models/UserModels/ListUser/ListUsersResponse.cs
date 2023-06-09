﻿using Domain.Models.UserModels;

namespace Domain.Models.UserModels.ListUser;

public class ListUsersResponse
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalUsers { get; set; }
    public List<UserResponse> Users { get; set; }
}
