(function() {
    app.filter('exercisesFilter', function() {
        return function (exercises, filterValue, muscularGroup) {
            if (!filterValue && muscularGroup === 'All')
                return exercises;
            var matches = [];

            for (var i = 0; i < exercises.length; i++) {
                var isTheRightMuscularGroup = muscularGroup === 'All' || exercises[i].MuscularGroup === muscularGroup;
                var containsSearchTerms = exercises[i].Name.toLowerCase().indexOf(filterValue.toLowerCase()) > -1;

                if (containsSearchTerms && isTheRightMuscularGroup) {
                    matches.push(exercises[i]);

                }
            }
            return matches;
        };
    });
})();