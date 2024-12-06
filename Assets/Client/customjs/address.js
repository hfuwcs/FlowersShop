$(document).ready(function () {
    // Load danh sách tỉnh/thành phố
    $.getJSON('/api/Location/GetProvinces', function (provinces) {
        provinces.forEach(function (province) {
            $('#customerProvince').append(
                $('<option>', { value: province.code, text: province.name })
            );
        });
    });

    // Khi người dùng chọn Tỉnh/Thành phố, load danh sách Quận/Huyện
    $('#customerProvince').change(function () {
        var provinceCode = $(this).val();
        $('#customerDistrict').empty().append('<option value="">Chọn Quận/Huyện</option>');
        $('#customerWard').empty().append('<option value="">Chọn Phường/Xã</option>').prop('disabled', true);

        if (provinceCode) {
            $('#customerDistrict').prop('disabled', false);

            $.getJSON('/api/Location/GetDistricts', { province_code: provinceCode }, function (districts) {
                districts.forEach(function (district) {
                    $('#customerDistrict').append(
                        $('<option>', { value: district.code, text: district.name })
                    );
                });
            });
        } else {
            $('#customerDistrict').prop('disabled', true);
        }
    });

    // Khi người dùng chọn Quận/Huyện, load danh sách Phường/Xã
    $('#customerDistrict').change(function () {
        var districtCode = $(this).val();
        $('#customerWard').empty().append('<option value="">Chọn Phường/Xã</option>');

        if (districtCode) {
            $('#customerWard').prop('disabled', false);

            $.getJSON('/api/Location/GetWards', { district_code: districtCode }, function (wards) {
                wards.forEach(function (ward) {
                    $('#customerWard').append(
                        $('<option>', { value: ward.code, text: ward.name })
                    );
                });
            });
        } else {
            $('#customerWard').prop('disabled', true);
        }
    });
});