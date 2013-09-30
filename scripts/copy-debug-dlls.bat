REM Copy SDL libraries.
copy %1\SDL2.dll %2\SDL2.dll
copy %1\SDL2_image.dll %2\SDL2_image.dll
copy %1\SDL2_ttf.dll %2\SDL2_ttf.dll

REM Copy required SDL support libraries.
copy %1\libfreetype-6.dll %2\libfreetype-6.dll
copy %1\libjpeg-9.dll %2\libjpeg-9.dll
copy %1\libtiff-5.dll %2\libtiff-5.dll
copy %1\libwebp-4.dll %2\libwebp-4.dll
copy %1\zlib1.dll %2\zlib1.dll

REM Copy required licensing information.
copy %1\LICENSE.freetype.txt %2\LICENSE.freetype.txt
copy %1\LICENSE.jpeg.txt %2\LICENSE.jpeg.txt
copy %1\LICENSE.png.txt %2\LICENSE.png.txt
copy %1\LICENSE.tiff.txt %2\LICENSE.tiff.txt
copy %1\LICENSE.webp.txt %2\LICENSE.webp.txt
copy %1\LICENSE.zlib.txt %2\LICENSE.zlib.txt
copy %1\README.txt %2\README.txt
copy %1\README-SDL.txt %2\README-SDL.txt