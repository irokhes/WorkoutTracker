(function() {
    app.filter('exercisesFilter', function() {
        return function (exercises, filterValue) {
            if (filterValue === '') {
                return exercises;
            }
            var matches = [];
            for (var i = 0; i < exercises.length; i++) {
                var containsSearchTerms = exercises[i].name.toLowerCase().indexOf(filterValue.toLowerCase()) > -1;
                if (containsSearchTerms) {
                    matches.push(exercises[i]);
                }
            }
            return matches;
        };
    });
})();