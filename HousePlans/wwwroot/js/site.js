// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function addFields() {

    var number = this.getElementById("formNumber").value;
    var floorContainer = this.getElementById("floors");
    var roomContainer = this.getElementById("rooms");

    //while (container.hasChildNodes()) {
    //    container.removeChild(container.lastChild);
    //}

    for (i = 0; i < number; i++) {

        roomContainer.appendChild(document.createElement("p").textContent = "Floor number " + (i + 1));
        floorContainer.appendChild(roomContainer);

        //var div = document.createElement("div");
        //var label = document.createElement("label");
        //var input = document.createElement("input"); 
        //var span = document.createElement("span");

        //div.setAttribute("class", "form-group");

        //label.setAttribute("asp-for", "House.Floors.Rooms.Name");

        //input.setAttribute("asp-for", "House.Floors.Rooms.Name"); 
        //input.setAttribute("class", "form-control"); 
        //input.setAttribute("placeholder", "Enter room name"); 

        //span.setAttribute("asp-validation-for", "House.Floors.Rooms.Name"); 
        //span.setAttribute("class", "small text-danger"); 

        //div.appendChild(label);
        //div.appendChild(input);
        //div.appendChild(span);

        //container.appendChild(div); 
    }
}


