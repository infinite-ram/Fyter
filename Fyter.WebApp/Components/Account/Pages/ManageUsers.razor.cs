using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class ManageUsers : ComponentBase
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; set; }

    private List<ApplicationUser>? users;
    private string? userToDeleteId;
    private string? userToDeleteEmail;
    private bool showDeleteConfirmation;

    protected override void OnInitialized()
    {
        users = UserManager.Users.ToList();
    }

    private void ConfirmDeleteUser(string userId, string email)
    {
        userToDeleteId = userId;
        userToDeleteEmail = email;
        showDeleteConfirmation = true;
    }

    private void CancelDelete()
    {
        userToDeleteId = null;
        userToDeleteEmail = null;
        showDeleteConfirmation = false;
    }

    private async Task DeleteUser()
    {
        if (string.IsNullOrEmpty(userToDeleteId))
            return;

        var user = await UserManager.FindByIdAsync(userToDeleteId);

        if (user != null)
        {
            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                users?.Remove(user);
            }
            else
            {
                // Log or display an error message if deletion failed
            }
        }

        CancelDelete(); // Close the confirmation modal
    }
}
