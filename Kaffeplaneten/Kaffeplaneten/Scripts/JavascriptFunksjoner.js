
function showLoginBox()
{
    if (document.getElementById('LogInBox'))
    {
        $('#LoggInBar').hide();
        $('#LoggOutBar').show();
    }
    
}
    
function showLogOutBox()
{
    if (document.getElementById('LogOut'))
    {
        $('#LoggOutBar').hide();
        $('#LoggInBar').show();
    }
}
