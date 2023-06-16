

        var showDetails = 0;

        document.getElementById('show-details-box').style.display = 'none';

        document.getElementById('no-route').style.display = 'none';

        const initMap = () => {
            var mapProp = {
                center: new google.maps.LatLng(10.855298, 106.785043),
                zoom: 17,
            };

            var directionsService = new google.maps.DirectionsService();
            var directionsRenderer = new google.maps.DirectionsRenderer();
            var distanceService = new google.maps.DistanceMatrixService();

            var map = new google.maps.Map(document.getElementById("google-map"), mapProp);
            directionsRenderer.setMap(map);

            var origin = document.getElementById('origin-add');
            var autocompleteOrigin = new google.maps.places.Autocomplete(origin);

            var dest = document.getElementById('dest-add');
            var autocompleteDest = new google.maps.places.Autocomplete(dest);

            console.log(autocompleteOrigin, autocompleteDest)

            autocompleteOrigin.addListener('place_changed', function () {
                var place = autocompleteOrigin.getPlace();

                console.log(place)
                console.log(document.getElementById('origin-add').value)

            });
            //an map,an dat xe
            document.getElementById('google-map').style.display = 'none';
            document.getElementById('save-trip-btn').style.display = 'none';
            document.getElementById('reloadtrang').style.display = 'none';

            document.getElementById('show-route-btn').addEventListener('click', () => {



                var placeOrigin = autocompleteOrigin.getPlace();
                console.log(placeOrigin)
                var placeDest = autocompleteDest.getPlace();
                console.log(placeDest)

                var start = document.getElementById('origin-add').value;
                var end = document.getElementById('dest-add').value;
                var selectedMode = 'DRIVING';

                var request = {
                    origin: start,
                    destination: end,
                    travelMode: selectedMode
                };





                directionsService.route(request, (result, status) => {
                    if (status == 'OK') {
                        console.log(result)
                        directionsRenderer.setDirections(result);
                        document.getElementById('show-details-box').style.display = 'none';
                        document.getElementById('show-route-btn').style.display = 'none';
                        document.getElementById('no-route').style.display = 'none';
                        document.getElementById('google-map').style.display = 'block';
                        document.getElementById('save-trip-btn').style.display = 'block';
                    } else {
                        directionsRenderer.setDirections({ routes: [] });
                        document.getElementById('show-details-box').style.display = 'none';
                        document.getElementById('no-route').style.display = 'block';
                        document.getElementById('reloadtrang').style.display = 'block';
                        document.getElementById('show-route-btn').style.display = 'none';
                    }
                });

                const distanceRequest = {
                    origins: [start],
                    destinations: [end],
                    travelMode: selectedMode,
                    avoidHighways: false,
                    avoidTolls: false,
                };
                var pricePerKm = document.getElementById('priceInput').value;
                var pricePerKmLow = document.getElementById('priceInputLow').value;
                distanceService.getDistanceMatrix(distanceRequest).then((response) => {
                    if (response !== null) {
                        if (response.rows[0].elements[0].status === 'OK') {
                            console.log(response.rows[0].elements[0].distance)
                            console.log(response.rows[0].elements[0].duration)
                            document.getElementById('from_place').innerText ="Điểm đi:   " + document.getElementById('origin-add').value
                            document.getElementById('to_place').innerText = "Điểm đến:  "+ document.getElementById('dest-add').value
                            document.getElementById('response_distance').innerText ="Khoảng cách:   " + response.rows[0].elements[0].distance.text
                            document.getElementById('response_time').innerText = "Thời gian:   " + response.rows[0].elements[0].duration.text
                            const distanceInKm = response.rows[0].elements[0].distance.value / 1000;
                            let price = 0;
                            if (distanceInKm <= 1) {
                                price = pricePerKmLow * 1.0;
                            } else {
                                price = distanceInKm * pricePerKm;
                            }
                            document.getElementById('result').innerText = "Giá:  " + price.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });

                            var time = $('#timepicker').val();
                            $('#timebook').text('Giờ đặt xe: ' + time);


                        }
                        else {
                            console.log('Route Distance not found')
                        }
                    }
                })
                    .catch((error) => {
                        console.log(error);
                    });

            });

        }


        window.initMap = initMap;
        // Future enhancements:
        // Add departure time
        // Add intermediate locations
       const saveTripBtn = document.getElementById('save-trip-btn');

        // Đăng ký sự kiện click cho button
       saveTripBtn.addEventListener('click', function() {
       // Lấy dữ liệu từ form
       const from = document.getElementById('origin-add').value.toString();
       const to = document.getElementById('dest-add').value.toString();
       const distance = document.getElementById('response_distance').innerText.toString();
       const time = document.getElementById('response_time').innerText.toString();
       const price = document.getElementById('result').innerText.replace(/\D/g, '');
       var timebook = $('#timepicker').val();