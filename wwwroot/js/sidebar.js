function toggleHiddenContent(showMoreButtonId, hideButtonId, hiddenContentId) {
    const showMoreButton = document.getElementById(showMoreButtonId);
    const hideButton = document.getElementById(hideButtonId);
    const hiddenContent = document.getElementById(hiddenContentId);

    showMoreButton.addEventListener('click', () => {
        hiddenContent.classList.remove('d-none');
        showMoreButton.classList.add('d-none');
        hideButton.classList.remove('d-none');
    });

    hideButton.addEventListener('click', () => {
        hiddenContent.classList.add('d-none');
        hideButton.classList.add('d-none');
        showMoreButton.classList.remove('d-none');
    });
}

function initializeSidebar() {
    toggleHiddenContent('show-more-friends', 'hide-friends', 'more-friends');
    toggleHiddenContent('show-more-rooms', 'hide-rooms', 'more-rooms');

    const collapseButton = document.getElementById('collapse-button');
    const sidebar = document.getElementById('sidebar');

    collapseButton.addEventListener('click', () => {
        sidebar.classList.toggle('collapsed');
    });
}

document.addEventListener('DOMContentLoaded', initializeSidebar);