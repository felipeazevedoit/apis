<!-- COMMON SCRIPTS -->
<script src="js/jquery-2.2.4.min.js"></script>
	<script src="js/common_scripts_min.js"></script>
	<script src="js/functions.js"></script>
	<script src="assets/validate.js"></script>

	<!-- Specific scripts -->
	<script src="js/tabs.js"></script>
	<script>
		new CBPFWTabs(document.getElementById('tabs'));
	</script>

	<script src="js/bootstrap3-wysihtml5.min.js"></script>
	<script type="text/javascript">
		$('.wysihtml5').wysihtml5({});
	</script>
	<script src="js/dropzone.min.js"></script>
	<script>
		if ($('.dropzone').length > 0) {
			Dropzone.autoDiscover = false;
			$("#photos").dropzone({
				url: "upload",
				addRemoveLinks: true
			});

			$("#logo_picture").dropzone({
				url: "upload",
				maxFiles: 1,
				addRemoveLinks: true
			});

			$(".menu-item-pic").dropzone({
				url: "upload",
				maxFiles: 1,
				addRemoveLinks: true
			});
		}
	</script>