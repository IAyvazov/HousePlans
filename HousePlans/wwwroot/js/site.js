// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addFields() {

    var number = +document.getElementById("numberOfFloors").value;

    var floors = document.getElementById("floors");

    for (var i = 0; i < number; i++) {

        var h2 = document.createElement("h2");
        h2.textContent = "Floor " + i;

        var div = document.createElement("div");
        div.setAttribute("class", "form-group");

        var labelNumber = document.createElement("label");
        labelNumber.setAttribute("name", `House.Floors[${i}].NumberOfRooms`);
        labelNumber.setAttribute("class", "control-label");
        labelNumber.textContent = "Number of rooms";

        var inputNumber = document.createElement("input");
        inputNumber.setAttribute("name", `House.Floors[${i}].NumberOfRooms`);
        inputNumber.setAttribute("id", "numberOfRooms");
        inputNumber.setAttribute("type", "number");
        inputNumber.setAttribute("step", "0.01");
        inputNumber.setAttribute("class", "form-control");
        inputNumber.addEventListener("change", addRoomFields);

        var divRooms = document.createElement("div");
        divRooms.setAttribute("id", `room${i}`);
        divRooms.setAttribute("class", "form-group");

        div.appendChild(h2);
        div.appendChild(labelNumber);
        div.appendChild(inputNumber);

        floors.appendChild(div);
        floors.appendChild(divRooms);
    }
}

function addRoomFields(e) {

    var number = +e.currentTarget.value;

    var rooms = e.target.parentNode.nextSibling;

    var floorNumber = +rooms.id.replace("room", "");;

    for (var i = 0; i < number; i++) {


        var h2 = document.createElement("h2");
        h2.textContent = "Room " + (i+1);

        var divName = document.createElement("div");
        divName.setAttribute("class", "form-group");

        var divArea = document.createElement("div");
        divArea.setAttribute("class", "form-group");

        var labelName = document.createElement("label");
        labelName.setAttribute("class", "control-label");
        labelName.setAttribute("name", `House.Floors[${floorNumber}].Rooms[${i}].Name`);
        labelName.textContent = "Room Name";

        var inputName = document.createElement("input");
        inputName.setAttribute("type", "text");
        inputName.setAttribute("class", "form-control");
        inputName.setAttribute("name", `House.Floors[${floorNumber}].Rooms[${i}].Name`);


        var labelArea = document.createElement("label");
        labelArea.setAttribute("class", "control-label");
        labelArea.setAttribute("name", `House.Floors[${floorNumber}].Rooms[${i}].Area`);
        labelArea.textContent = "Room Area";

        var inputArea = document.createElement("input");
        inputArea.setAttribute("type", "number");
        inputArea.setAttribute("step", "0.01");
        inputArea.setAttribute("class", "form-control");
        inputArea.setAttribute("name", `House.Floors[${floorNumber}].Rooms[${i}].Area`);

        
        divName.appendChild(labelName);
        divName.appendChild(inputName);

        divArea.appendChild(labelArea);
        divArea.appendChild(inputArea);

        rooms.appendChild(h2);
        rooms.appendChild(divName);
        rooms.appendChild(divArea);
    }
}
