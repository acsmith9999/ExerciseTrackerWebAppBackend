<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
	version="1.0"
	xmlns:dept="http://tempuri.org/ExerciseTracker"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	exclude-result-prefixes="msxsl dept"
>
	<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<table class="people-table">
			<thead>
				<tr>
					<th>ID</th>
					<th>Type</th>
					<th>Date</th>
					<th>Duration</th>
					<th>Distance</th>
				</tr>
			</thead>
			<tbody>
				<xsl:for-each select="//dept:Activity" xml:space="preserve">
					<xsl:variable name="activityId" select="dept:TypeId"></xsl:variable>
					<tr>
						<td><xsl:value-of select="@Id"/></td>
						<td>
							<xsl:value-of select="document('Types.xml')/dept:Types/dept:Type[@Id=$activityId]/dept:Name"/>
							<!--<xsl:value-of select="dept:TypeId"/>-->
						</td>
						<td><xsl:value-of select="dept:Date"/></td>
						<td><xsl:value-of select="dept:Duration"/></td>
						<td><xsl:value-of select="dept:Distance"/></td>
					</tr>
				</xsl:for-each>
			</tbody>
		</table>
	</xsl:template>
</xsl:stylesheet>
