/// <binding Clean='clean' />
"use strict";

const gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    sass = require("gulp-sass");

const paths = {
    jsSource: "./Scripts/",
    cssSource: "./Styles/",
    webroot: "./wwwroot/",
    modules: "./node_modules/"
};

paths.bootstrapJs = paths.modules + "bootstrap/dist/js/bootstrap.bundle.min.js";
paths.bootstrapCss = paths.modules + "bootstrap/dist/css/bootstrap.min.css";
paths.jquery = paths.modules + "jquery/dist/jquery.min.js";
paths.js = paths.jsSource + "**/*.js";
paths.minJs = paths.jsSource + "**/*.min.js";
paths.css = paths.cssSource + "**/*.css";
paths.minCss = paths.cssSource + "**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.concatBootstrapJsDest = paths.webroot + "js/bootstrap.bundle.min.js";
paths.concatBootstrapCssDest = paths.webroot + "css/bootstrap.bundle.min.css";
paths.concatJqueryDest = paths.webroot + "js/jquery.min.js";

gulp.task("clean:js", done => rimraf(paths.concatJsDest, done));
gulp.task("clean:css", done => rimraf(paths.concatCssDest, done));
gulp.task("clean", gulp.series(["clean:js", "clean:css"]));

gulp.task("min:js", () => {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", () => {
    return gulp.src([paths.css, "!" + paths.minCss], { base: "." })
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", gulp.series(["min:js", "min:css"]));

gulp.task("sass", () => {
    return gulp.src("./Styles/*.scss")
        .pipe(sass())
        .pipe(gulp.dest(paths.cssSource));
});

gulp.task("bootstrap", () => {
    return gulp.src(paths.bootstrapJs)
        .pipe(concat(paths.concatBootstrapJsDest))
        .pipe(gulp.dest("."))
        .pipe(gulp.src(paths.bootstrapCss))
        .pipe(concat(paths.concatBootstrapCssDest))
        .pipe(gulp.dest("."));
});

gulp.task("jquery", () => {
    return gulp.src(paths.jquery)
        .pipe(concat(paths.concatJqueryDest))
        .pipe(gulp.dest("."));
});

// A 'default' task is required by Gulp v4
gulp.task("default", gulp.series(["sass", "min", "bootstrap", "jquery"]));