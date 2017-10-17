
function getDistances()
{
    var allElements = $(".timeline-entity")
    var dates = new string[allElements.length][4]

    for (var i = 0; i < allElements.length; i++)
    {
        dates[i] = allElements[i].id.split("-")
        allElements[i].innerHTML = allElements[i].id
    }
}