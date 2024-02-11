// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function updateApprovalStatus(element) {
    const teamId = element.dataset.teamid;
    let approved = parseInt(element.dataset.approved);
    let flag = parseInt(element.dataset.flag);

    if (approved === 0) {
        // First click: Change to green circle and approved = 1
        element.style.backgroundColor = 'green';
        approved = 1;
    } else if (approved === 1) {
        // Second click: Change to cross circle and approved = 2
        element.style.backgroundColor = 'red';
        approved = 2;
    } else {
        // Third click: Change to gray circle and approved = 0
        element.style.backgroundColor = 'gray';
        approved = 0;
    }

    element.dataset.approved = approved;

    try {
        const response = await fetch(`/Home/UpdateApprovalStatus?teamId=${teamId}&approved=${approved}&flag=${flag}`, {
            method: 'POST',
            
        });



        if (!response.ok) {
            throw new Error('Failed to update approval status');
        }

        const data = await response.json();
        console.log(data); // log the response for debugging
       
    } catch (error) {
        console.error('Error updating approval status:', error);
    }
}

