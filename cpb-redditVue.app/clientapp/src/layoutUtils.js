export default {

    // Masonry type variable hight grid layout, adapted from: https://medium.com/@andybarefoot/a-masonry-style-layout-using-css-grid-8c663d355ebb

    resizeGridItem(item) {
        let grid = document.getElementsByClassName("grid")[0];
        let rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
        let rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'));
        let rowSpan = Math.ceil((item.querySelector('.content').getBoundingClientRect().height + rowGap) / (rowHeight + rowGap));
        item.style.gridRowEnd = "span " + rowSpan;
    },
    resizeAllGridItems() {
        let allItems = document.getElementsByClassName("item");
        for (let x = 0; x < allItems.length; x++) {
            this.resizeGridItem(allItems[x]);
        }
    }
};